import '../../core/api/api_client.dart';

class AuthApi {
  static Future<Map<String, dynamic>> login({
    required String phoneNumber,
    required String fullName,
  }) async {
    final res = await ApiClient.post(
      '/auth/login',
      data: {
        'phoneNumber': phoneNumber,
        'fullName': fullName,
      },
    );

    return Map<String, dynamic>.from(res);
  }
}
