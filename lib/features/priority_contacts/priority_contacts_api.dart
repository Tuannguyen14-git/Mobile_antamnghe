import '../../core/api/api_client.dart';

class PriorityContactsApi {
  static Future<List<dynamic>> getAll(int userId) async {
    final res = await ApiClient.get(
      '/api/PriorityContacts/user/$userId',
    );
    return res.data;
  }

  static Future<void> create(Map<String, dynamic> data) async {
    await ApiClient.post(
      '/api/PriorityContacts',
      data: data,
    );
  }

  static Future<void> update(int id, Map<String, dynamic> data) async {
    await ApiClient.put(
      '/api/PriorityContacts/$id',
      data: data,
    );
  }

  static Future<void> delete(int id) async {
    await ApiClient.delete(
      '/api/PriorityContacts/$id',
    );
  }

  static Future<void> toggleActive(int id, bool isActive) async {
    await ApiClient.put(
      '/api/PriorityContacts/$id/active',
      data: {
        'isActive': isActive,
      },
    );
  }
}
