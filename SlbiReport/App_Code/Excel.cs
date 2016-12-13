using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;

public class Excel
{
	public Excel()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public void dataTableToCsv(DataTable table, string file)

    {

        string title = "";

        FileStream fs = new FileStream(file, FileMode.OpenOrCreate);

        //FileStream fs1 = File.Open(file, FileMode.Open, FileAccess.Read);

        StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

        for (int i = 0; i < table.Columns.Count; i++)

        {

            title += table.Columns[i].ColumnName + "\t"; //栏位：自动跳到下一单元格

        }

        title = title.Substring(0, title.Length - 1) + "\n";

        sw.Write(title);

        foreach (DataRow row in table.Rows)

        {

            string line = "";

            for (int i = 0; i < table.Columns.Count; i++)

            {

                line += row[i].ToString().Trim() + "\t"; //内容：自动跳到下一单元格

            }

            line = line.Substring(0, line.Length - 1) + "\n";

            sw.Write(line);

        }

        sw.Close();

        fs.Close();

    }


}
