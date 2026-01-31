import 'package:flutter/material.dart';
import '../../core/storage/local_storage.dart';
import 'priority_contacts_api.dart';

class PriorityContactForm extends StatefulWidget {
  final Map<String, dynamic>? contact;
  const PriorityContactForm({super.key, this.contact});

  @override
  State<PriorityContactForm> createState() => _PriorityContactFormState();
}

class _PriorityContactFormState extends State<PriorityContactForm> {
  final _nameCtrl = TextEditingController();
  final _phoneCtrl = TextEditingController();
  bool isEdit = false;

  @override
  void initState() {
    super.initState();
    if (widget.contact != null) {
      isEdit = true;
      _nameCtrl.text = widget.contact!['name'];
      _phoneCtrl.text = widget.contact!['phoneNumber'];
    }
  }

  Future<void> _save() async {
    final user = await LocalStorage.getUser();
    if (user == null) return;

    if (isEdit) {
      await PriorityContactsApi.update(
        widget.contact!['id'],
        {
          'name': _nameCtrl.text,
          'phoneNumber': _phoneCtrl.text,
        },
      );
    } else {
      await PriorityContactsApi.create({
        'userId': user['userId'],
        'name': _nameCtrl.text,
        'phoneNumber': _phoneCtrl.text,
        'priorityLevel': 1,
        'isActive': true,
      });
    }

    if (!mounted) return;
    Navigator.pop(context, true); // trả về để reload list
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(isEdit ? 'Sửa liên hệ' : 'Thêm liên hệ'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            TextField(
              controller: _nameCtrl,
              decoration: const InputDecoration(labelText: 'Tên'),
            ),
            const SizedBox(height: 12),
            TextField(
              controller: _phoneCtrl,
              decoration: const InputDecoration(labelText: 'Số điện thoại'),
              keyboardType: TextInputType.phone,
            ),
            const SizedBox(height: 24),
            ElevatedButton(
              onPressed: _save,
              child: const Text('Lưu'),
            )
          ],
        ),
      ),
    );
  }
}
