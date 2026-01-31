import 'package:dio/dio.dart';

class ApiClient {
  static final Dio _dio = Dio(
    BaseOptions(
      baseUrl: 'http://localhost:5036/api',
      connectTimeout: const Duration(seconds: 10),
      receiveTimeout: const Duration(seconds: 10),
      headers: {
        'Content-Type': 'application/json',
      },
    ),
  );

  // ===== GET =====
  static Future<dynamic> get(String path) async {
    final response = await _dio.get(path);
    return response.data;
  }

  // ===== POST =====
  static Future<dynamic> post(
    String path, {
    dynamic data,
  }) async {
    final response = await _dio.post(path, data: data);
    return response.data;
  }

  // ===== PUT =====
  static Future<dynamic> put(
    String path, {
    dynamic data,
  }) async {
    final response = await _dio.put(path, data: data);
    return response.data;
  }

  // ===== DELETE =====
  static Future<void> delete(String path) async {
    await _dio.delete(path);
  }
}
