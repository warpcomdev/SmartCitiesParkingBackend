﻿namespace SCParking.Domain.Enums
{
    public enum ECodeResult
    {
        RESULT_LEAD_INSERTION_PROCESS_STARTED = 0,
        RESULT_LEAD_DTO_CONVERSION_STARTED = 1,
        RESULT_LEAD_DTO_CONVERSION_OK = 2,
        RESULT_LEAD_DTO_CONVERSION_FAIL = 3,
        RESULT_LEAD_VALIDATION_STARTED = 4,
        RESULT_LEAD_VALIDATION_OK = 5,
        RESULT_LEAD_VALIDATION_FAIL = 6,
        RESULT_LEAD_DEDUPLICATION_STARTED = 7,
        RESULT_LEAD_DEDUPLICATION_OK = 8,
        RESULT_LEAD_DEDUPLICATION_FAIL = 9,
        RESULT_LEAD_PRIORIZATION_STARTED = 10,
        RESULT_LEAD_PRIORIZATION_OK = 11,
        RESULT_LEAD_PRIORIZATION_FAIL = 12,
        RESULT_LEAD_DB_INSERTION_STARTED = 13,
        RESULT_LEAD_DB_INSERTION_OK = 14,
        RESULT_LEAD_DB_INSERTION_FAIL = 15,
        RESULT_LEAD_WS_INSERTION_STARTED = 16,
        RESULT_LEAD_WS_INSERTION_OK = 17,
        RESULT_LEAD_WS_INSERTION_FAIL = 18,
        RESULT_LEAD_INSERTION_PROCESS_FINISHED = 19,
        RESULT_UNEXPECTED_ERROR = 20,
        RESULT_LEAD_WS_GET_MATOMO_DATA_STARTED = 21,
        RESULT_LEAD_WS_GET_MATOMO_DATA_OK = 22,
        RESULT_LEAD_WS_GET_MATOMO_DATA_FAIL = 23,
        RESULT_LEAD_NOT_IMPLEMENTED = 24,
        RESULT_LEAD_KEY_GENERATION_STARTED = 25,
        RESULT_LEAD_KEY_GENERATION_OK = 26,
        RESULT_LEAD_KEY_GENERATION_FAIL = 27,
        RESULT_LEAD_VALIDATION_ROBINSON_STARTED = 28,
        RESULT_LEAD_VALIDATION_ROBINSON_OK = 29,
        RESULT_LEAD_VALIDATION_ROBINSON_FAIL = 30,
        RESULT_LEAD_DB_UPDATE_OK = 31,
        RESULT_LEAD_DB_UPDATE_FAIL = 32,
        RESULT_CVAR_DB_INSERT_OK = 33,
        RESULT_CVAR_DB_INSERT_FAIL = 34,
        RESULT_FORM_FIELD_DB_INSERT_OK = 35,
        RESULT_FORM_FIELD_DB_INSERT_FAIL = 36,
        RESULT_LEAD_DUPLICATED = 37,
        RESULT_LEAD_INVALID_PHONE_NUMBER = 38,
        RESULT_LEAD_NO_VISITORID = 39,
        RESULT_LEAD_NO_FORMID = 40,
        RESULT_LEAD_IS_ROBINSON = 41,
        RESULT_LEAD_NO_CAMPAIGN_SITE = 42
    }
}