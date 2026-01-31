import 'package:shared_preferences/shared_preferences.dart';

class LocalStorage {
  static const _keyUserId = 'userId';
  static const _keyName = 'name';
  static const _keyPhone = 'phone';
  static const _keyFilterMode = 'filterMode';

  /// 🔐 SAVE USER
  static Future<void> saveUser({
    required int userId,
    required String name,
    required String phone,
    required String filterMode,
  }) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.setInt(_keyUserId, userId);
    await prefs.setString(_keyName, name);
    await prefs.setString(_keyPhone, phone);
    await prefs.setString(_keyFilterMode, filterMode);
  }

  /// 📥 GET USER
  static Future<Map<String, dynamic>?> getUser() async {
    final prefs = await SharedPreferences.getInstance();
    final userId = prefs.getInt(_keyUserId);

    if (userId == null) return null;

    return {
      'userId': userId,
      'name': prefs.getString(_keyName),
      'phone': prefs.getString(_keyPhone),
      'filterMode': prefs.getString(_keyFilterMode) ?? 'Allowed',
    };
  }

  /// ⚙️ UPDATE FILTER MODE
  static Future<void> setFilterMode(String mode) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString(_keyFilterMode, mode);
  }

  /// 🚪 LOGOUT
  static Future<void> clear() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.clear();
  }

  static Future<void> saveFilterMode(String value) async {}

  static Future<dynamic> getFilterMode() async {}
}
