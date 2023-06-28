public class CalculateBMIHandler
{
    public BMIResponse Handle(BMIRequest request)
    {
        // Convert height to meters
        double heightInMeters = request.Height / 100;
        var result = new BMIResponse();
        // Calculate BMI
        result.BMI = request.Weight / (heightInMeters * heightInMeters);
        if (result.BMI < 18.5)
        {
            result.BMIHealth = "Underweight";
        }
        else if (result.BMI >= 18.5 && result.BMI < 25)
        {
            result.BMIHealth = "Normal weight";
        }
        else if (result.BMI >= 25 && result.BMI < 30)
        {
            result.BMIHealth = "Overweight";
        }
        else
        {
            result.BMIHealth = "Obese";
        }
        return result;
    }
}
public class BMIRequest
{
    public double Weight { get; set; }
    public double Height { get; set; }
}
public class BMIResponse
{
    public double BMI { get; set; }
    public string BMIHealth { get; set; }
}
