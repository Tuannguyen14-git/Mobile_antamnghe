import 'package:flutter/material.dart';
import '../../core/storage/local_storage.dart';
import 'priority_contacts_api.dart';
import 'priority_contact_form.dart';

class PriorityContactsPage extends StatefulWidget {
  const PriorityContactsPage({super.key});

  @override
  State<PriorityContactsPage> createState() => _PriorityContactsPageState();
}

class _PriorityContactsPageState extends State<PriorityContactsPage> {
  List<dynamic> contacts = [];
  bool loading = true;

  @override
  void initState() {
    super.initState();
    _loadContacts();
  }

  Future<void> _loadContacts() async {
    final user = await LocalStorage.getUser();
    final data =
        await PriorityContactsApi.getAll(user!['userId']);

    setState(() {
      contacts = data;
      loading = false;
    });
  }

  Future<void> _toggleActive(int index, bool value) async {
    final contact = contacts[index];

    setState(() {
      contacts[index]['isActive'] = value;
    });

    await PriorityContactsApi.toggleActive(
      contact['id'],
      value,
    );
  }

  Future<void> _deleteContact(int index) async {
    final id = contacts[index]['id'];
    setState(() {
      contacts.removeAt(index);
    });
    await PriorityContactsApi.delete(id);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Danh bạ ưu tiên'),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          await Navigator.push(
            context,
            MaterialPageRoute(
              builder: (_) => const PriorityContactForm(),
            ),
          );
          _loadContacts();
        },
        child: const Icon(Icons.add),
      ),
      body: loading
          ? const Center(child: CircularProgressIndicator())
          : contacts.isEmpty
              ? const Center(child: Text('Chưa có liên hệ'))
              : ListView.separated(
                  padding: const EdgeInsets.all(16),
                  itemCount: contacts.length,
                  separatorBuilder: (_, __) =>
                      const SizedBox(height: 12),
                  itemBuilder: (context, index) {
                    final c = contacts[index];
                    final active = c['isActive'] == true;

                    return Dismissible(
                      key: ValueKey(c['id']),
                      direction: DismissDirection.endToStart,
                      background: Container(
                        alignment: Alignment.centerRight,
                        padding:
                            const EdgeInsets.only(right: 20),
                        decoration: BoxDecoration(
                          color: Colors.red.shade500,
                          borderRadius:
                              BorderRadius.circular(16),
                        ),
                        child: const Icon(
                          Icons.delete,
                          color: Colors.white,
                        ),
                      ),
                      onDismissed: (_) => _deleteContact(index),
                      child: _buildContactCard(
                        name: c['name'],
                        phone: c['phoneNumber'],
                        active: active,
                        onToggle: (v) =>
                            _toggleActive(index, v),
                        onEdit: () async {
                          await Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (_) =>
                                  PriorityContactForm(
                                contact: c,
                              ),
                            ),
                          );
                          _loadContacts();
                        },
                      ),
                    );
                  },
                ),
    );
  }

  Widget _buildContactCard({
    required String name,
    required String phone,
    required bool active,
    required ValueChanged<bool> onToggle,
    required VoidCallback onEdit,
  }) {
    return Container(
      padding: const EdgeInsets.all(16),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(16),
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.05),
            blurRadius: 10,
            offset: const Offset(0, 6),
          ),
        ],
      ),
      child: Row(
        children: [
          /// DOT STATUS
          Container(
            width: 12,
            height: 12,
            decoration: BoxDecoration(
              color: active ? Colors.green : Colors.grey,
              shape: BoxShape.circle,
            ),
          ),
          const SizedBox(width: 12),

          /// INFO
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  name,
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w600,
                  ),
                ),
                const SizedBox(height: 4),
                Text(
                  phone,
                  style: const TextStyle(
                    color: Colors.grey,
                    fontSize: 13,
                  ),
                ),
              ],
            ),
          ),

          /// TOGGLE
          Switch(
            value: active,
            onChanged: onToggle,
          ),

          /// EDIT
          IconButton(
            icon: const Icon(Icons.edit, size: 20),
            onPressed: onEdit,
          ),
        ],
      ),
    );
  }
}
