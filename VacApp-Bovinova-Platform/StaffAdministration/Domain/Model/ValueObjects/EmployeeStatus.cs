namespace VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.ValueObjects;

public record EmployeeStatus
{
    public int Value { get; private set; }

    public EmployeeStatus(int value)
    {
        if (value < 1 || value > 5)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value), 
                "EmployeeStatus must be:\n" + 
                " 1 : Active,\n " +
                "2 : Inactive,\n " +
                "3 : OnLeave,\n " +
                "4 : Retired,\n " +
                "5 : Terminated\n"
            );
        }
        Value = value;
    }

    public EmployeeStatus() : this(1) { }
}