class ApiEndpoints {
  // Android Emulator
  static const String baseUrl = 'http://10.0.2.2:5036/api';

  // Auth
  static const String login = '$baseUrl/auth/login';

  // Priority Contacts
  static const String priorityContacts = '$baseUrl/PriorityContacts';

  // Call Logs
  static const String callLogs = '$baseUrl/CallLogs';

  // User Settings
  static const String userSettings = '$baseUrl/UserSettings';
}
