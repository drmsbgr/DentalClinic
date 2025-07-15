using DCAPPLIB.Entities;

namespace DCAPPLIB;

public static class Extensions
{
    public static string FullName(this Dentist dentist) => dentist.FirstName + " " + dentist.LastName?.ToUpper();
}