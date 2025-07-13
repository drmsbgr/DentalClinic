namespace DCAPI.Exceptions;

public sealed class DentistNotFoundException() : NotFoundException("Belirtilen diş hekimi bulunamadı!")
{
}