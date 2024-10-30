using System.Data;

namespace Dal.Common;

public delegate T RowMapper<T>(IDataRecord reader);