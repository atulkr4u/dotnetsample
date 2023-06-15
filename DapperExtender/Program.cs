using System;
using System.Reflection;
using DapperExtender.DapperExtention;

public class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            ActiveSessionResult result = DapperWrapper.Query<ActiveSessionResult>(new ActiveSessionQuery { EmployeeID = "BPW41175" });
            Console.WriteLine(result.SessionId);

            Console.WriteLine("---------");
            List<ActiveSessionResult> result2 = DapperWrapper.QueryAll<ActiveSessionResult>(new ActiveAllSessionQuery { EmployeeID = "BPW41175" }).Result.ToList();
            var top5 = result2.Take(5);
            foreach (var a in top5)
            {
                Console.WriteLine(a.SessionId);
            }


        }
        catch(Exception ex)
        {
            Console.Write(ex.Message);
        }
        Console.ReadLine();

    }
}
[QueryName("[BMS2].[GetActiveSession]")]
public class ActiveSessionQuery 
{
    public string EmployeeID { get; set; }
}
[QueryName("[BMS2].[GetAllActiveSession]")]
public class ActiveAllSessionQuery
{
    public string EmployeeID { get; set; }
}
public class ActiveSessionResult
{
    public string SessionId { get; set; }
    public int UserID { get; set; }
}