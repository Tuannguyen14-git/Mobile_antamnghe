enum FilterMode {
  allowed,
  blocked,
  silent,
}

extension FilterModeExtension on FilterMode {
  String get label {
    switch (this) {
      case FilterMode.allowed:
        return 'Cho phép tất cả';
      case FilterMode.blocked:
        return 'Chặn số lạ';
      case FilterMode.silent:
        return 'Im lặng (không chuông)';
    }
  }

  String get value => name;

  static FilterMode fromString(String value) {
    return FilterMode.values.firstWhere(
      (e) => e.name == value,
      orElse: () => FilterMode.allowed,
    );
  }
}
