import '../../core/storage/local_storage.dart';
import '../../core/constants/filter_mode.dart';

class CallLogsApi {
  static Future<Map<String, dynamic>> simulateCall(
    int userId,
    String phoneNumber,
  ) async {
    final modeStr = await LocalStorage.getFilterMode();
    final mode = FilterModeExtension.fromString(modeStr);

    String result;

    switch (mode) {
      case FilterMode.allowed:
        result = 'ALLOWED';
        break;
      case FilterMode.blocked:
        result = 'BLOCKED';
        break;
      case FilterMode.silent:
        result = 'SILENT';
        break;
    }

    // 👉 Sau này có thể gọi API backend lưu CallLog
    return {
      'userId': userId,
      'phone': phoneNumber,
      'result': result,
      'time': DateTime.now().toIso8601String(),
    };
  }

  static Future<dynamic> getLogs(param0) async {}

  static Future<dynamic> getAll(param0) async {}
}
