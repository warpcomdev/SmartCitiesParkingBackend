namespace SCParking.Domain.Messages
{
    public static class Message
    {
        public static string MSG_INSERT_LEAD_OK = "Lead se insertó correctamente";
        public static string MSG_INSERT_LEAD_FAIL = "Fallo durante la inserción de lead";
        public static string MSG_UPDATE_LEAD_OK = "Lead se actualizó exitosamente";
        public static string MSG_UPDATE_LEAD_FAIL = "Fallo durante la actualización de lead";
        public static string MSG_INSERT_UPDATE_LEAD_OK = "Lead insertado o actualizado correctamente";
        public static string MSG_INSERT_UPDATE_LEAD_FAIL = "Fallo durante la inserción o actualización de lead";
        public static string MSG_WRONG_FORMAT = "Formato incorrecto";
        public static string MSG_CHECK_DUPLICATE = "Fallo en proceso de verificación de duplicados";
        public static string MSG_SET_PRIORITY = "Fallo en proceso de asignación de prioridad";
        public static string MSG_DTO_CONVERSION_OK = "Proceso de conversion a Dto OK";
        public static string MSG_DTO_CONVERSION_FAIL = "Fallo en proceso de conversión a Dto";
        public static string MSG_VALIDATION_OK = "Proceso de validación OK";
        public static string MSG_VALIDATION_FAIL = "Fallo en proceso de validación";
        public static string MSG_DEDUPLICATION_OK = "Proceso de deduplicación OK";
        public static string MSG_DEDUPLICATION_FAIL = "Fallo en proceso de deduplicación";
        public static string MSG_PRIORIZATION_OK = "Proceso de priorización OK";
        public static string MSG_PRIORIZATION_FAIL = "Fallo en proceso de priorización";
        public static string MSG_DB_INSERT_OK = "Proceso de inserción a BD OK";
        public static string MSG_DB_INSERT_FAIL = "Fallo en proceso de inserción a BD";
        public static string MSG_WS_INSERT_OK = "Proceso de inserción sobre WS OK";
        public static string MSG_WS_INSERT_FAIL = "Fallo en proceso de inserción sobre WS";
        public static string MSG_PROCESS_STARTED = "Inicio del proceso de inserción";
        public static string MSG_PROCESS_FINISHED = "Proceso de inserción de lead finalizado correctamente.";
        public static string MSG_PROCESS_UNEXPECTED_ERROR = "Error inesperado durante el proceso de inserción";
        public static string CFG_SERVICEID_MOVIL = "MOVIL";
        public static string CFG_SERVICEID_FIXED = "FIJA";
        public static string MSG_DUPLICATE_FOUND = "Ya existe un registro con las mismas características";
        public static string MSG_GET_MATOMO_DATA_OK = "Proceso de obtención de datos desde Matomo OK";
        public static string MSG_GET_MATOMO_DATA_FAIL = "Fallo en proceso de obtención de datos desde Matomo ";
        public static string MSG_LEAD_INITIALIZATION_FAIL = "Fallo en inicialización de lead";
        public static string MSG_KEYGENERATION_STARTED = "Inicio del proceso de generación de llave";
        public static string MSG_KEYGENERATION_OK = "Proceso de generación de llave finalizado correctamente";
        public static string MSG_KEYGENERATION_FAIL = "Fallo en proceso de generación de llave ";
        public static string MSG_ROBINSON_STARTED = "Inicio de verificación listas Robinson";
        public static string MSG_ROBINSON_FOUND = "El telefono no se encuentra habilitado para marcación (políticas anti-spam)";
        public static string MSG_ROBINSON_OK = "Proceso de verificación lista Robinson OK";
        public static string MSG_ROBINSON_FAIL = "Fallo en proceso de verificación lista Robinson";
        public static string MSG_INSERT_CVAR_OK = "Cvar se insertó correctamente";
        public static string MSG_INSERT_CVAR_FAIL = "Fallo durante la inserción de Cvar";
        public static string MSG_INSERT_FORM_FIELD_OK = "Form Field se insertó correctamente";
        public static string MSG_INSERT_FORM_FIELD_FAIL = "Fallo durante la inserción de Form Field";
        public static string MSG_LEAD_DUPLICATED = "Lead duplicado";
        public static string MSG_LEAD_INVALID_PHONE = "Número de teléfono inválido";
        public static string MSG_LEAD_NO_VISITORID = "El lead no contiene Visitor Id";
        public static string MSG_LEAD_NO_FORMID = "El lead no contiene Form Id";
        public static string MSG_LEAD_IS_ROBINSON = "El Lead se encuentra en la lista Robinson";
        public static string MSG_LEAD_VALIDATION_OK = "El Lead es válido para inserción";
        public static string MSG_LAIA_TOKEN_NO_CAMPAIGN_SITE = "El Laia token no posee un campaign site";
    }
}
