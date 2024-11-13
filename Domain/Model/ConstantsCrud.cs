namespace crudTest.Domain.Model
{
    public class ConstantsCrud
    {
        public struct Success
        {
            public const string code2000 = "PS-ENRO-2000";
            public const string Message2000 = "Se obtuvo la información correctamente";

            public const string code2001 = "PS-ENRO-2001";
            public const string Message2001 = "Se registro el usuario correctamente";

            public const string code2002 = "PS-ENRO-2002";
            public const string Message2002 = "Se edito el usuario correctamente";

            public const string code2003 = "PS-ENRO-2003";
            public const string Message2003 = "Se elimino el usuario correctamente";
        }

        public struct ErrorRequest
        {
            public static string code4000 { get; } = "PS-ENRO-4000";
            public static string Message4000 { get; } = "No se ha ingresado los valores requeridos.";
            public static string code4009 { get; } = "PS-ENRO-4009";
            public static string Message4009 { get; } = "Conflicto";
        }

        public struct ErrorInterno
        {
            public static string code5000 { get; } = "PS-ENRO-5000";
            public static string Message5000 { get; } = "Respuesta inesperada del servicio.";
        }
    }
}
