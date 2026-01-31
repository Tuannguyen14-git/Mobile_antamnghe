import 'package:flutter/material.dart';
import '../../core/constants/filter_mode.dart';
import '../../core/storage/local_storage.dart';

class SettingsPage extends StatefulWidget {
  const SettingsPage({super.key});

  @override
  State<SettingsPage> createState() => _SettingsPageState();
}

class _SettingsPageState extends State<SettingsPage> {
  FilterMode _mode = FilterMode.allowed;
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadMode();
  }

  Future<void> _loadMode() async {
    final saved = await LocalStorage.getFilterMode();
    setState(() {
      _mode = FilterModeExtension.fromString(saved);
      isLoading = false;
    });
  }

  Future<void> _changeMode(FilterMode mode) async {
    setState(() {
      _mode = mode;
    });

    await LocalStorage.saveFilterMode(mode.value);

    if (!mounted) return;

    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text('Đã chuyển sang: ${mode.label}'),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    if (isLoading) {
      return const Scaffold(
        body: Center(child: CircularProgressIndicator()),
      );
    }

    return Scaffold(
      appBar: AppBar(
        title: const Text('Cài đặt'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Text(
              'Chế độ lọc cuộc gọi',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 12),

            _buildRadio(FilterMode.allowed),
            _buildRadio(FilterMode.blocked),
            _buildRadio(FilterMode.silent),

            const SizedBox(height: 24),

            const Text(
              '📌 Ghi chú:',
              style: TextStyle(fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 8),
            const Text(
              '- Cho phép: tất cả cuộc gọi\n'
              '- Chặn: số không trong danh bạ ưu tiên\n'
              '- Im lặng: không chuông, vẫn lưu nhật ký',
              style: TextStyle(color: Colors.grey),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildRadio(FilterMode mode) {
    return RadioListTile<FilterMode>(
      value: mode,
      groupValue: _mode,
      title: Text(mode.label),
      onChanged: (value) {
        if (value != null) {
          _changeMode(value);
        }
      },
    );
  }
}
