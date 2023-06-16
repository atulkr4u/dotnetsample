using System;
using System.Reflection;
using DapperExtender.DapperExtention;

public class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var result = await  new ActiveSessionQuery { EmployeeID = "BPW41175" }.Query<ActiveSessionResult>("[BMS2].[GetActiveSession]");
            Console.WriteLine(result.SessionId);

            Console.WriteLine("---------");
            var resultAll = await new ActiveSessionQuery { EmployeeID = "BPW41175" }.QueryAll<ActiveSessionResult>("[BMS2].[GetAllActiveSession]");
            var top5 = resultAll.Take(5);
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
public class ActiveSessionQuery 
{
    public string EmployeeID { get; set; }
}
public class ActiveSessionResult
{
    public string SessionId { get; set; }
    public int UserID { get; set; }
}
