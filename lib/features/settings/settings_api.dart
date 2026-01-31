import '../../core/api/api_client.dart';

class SettingsApi {
  static Future<String> getFilterMode(int userId) async {
    final res = await ApiClient.get('/settings/$userId');
    return res['filterMode'];
  }

  static Future<void> updateFilterMode(
    int userId,
    String mode,
  ) async {
    await ApiClient.put(
      '/settings/$userId',
      data: {'filterMode': mode},
    );
  }
}
