namespace ShopFlexByte.Domain.Common;

public static class DomainErrors
{
    public static class Customer
    {
        public static readonly string NotFound = "Cliente não encontrado.";
        public static readonly string LoginAlreadyRegistered = "Cliente já cadastrado.";
        public static readonly string BlockedAccount = "Conta bloqueada. Tente novamente mais tarde.";
    }
}
