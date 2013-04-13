using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1.sAccessDataBase
{
    public interface SyntaxInterface
    {
        string DropTable(string TableName, string DatabaseName);

        string CreateTable(string TableName, string DatabaseName);
    }
}
