namespace Usb.Hid.Connection.Win32Api
{
    // FACILITY_HID_ERROR_CODE defined in ntstatus.h
    //#ifndef FACILITY_HID_ERROR_CODE
    //#define FACILITY_HID_ERROR_CODE 0x11
    //#endif

    //#define HIDP_ERROR_CODES(SEV, CODE) \
    //        ((NTSTATUS) (((SEV) << 28) | (FACILITY_HID_ERROR_CODE << 16) | (CODE)))

    //#define HIDP_STATUS_SUCCESS                  (HIDP_ERROR_CODES(0x0,0))
    //#define HIDP_STATUS_NULL                     (HIDP_ERROR_CODES(0x8,1))
    //#define HIDP_STATUS_INVALID_PREPARSED_DATA   (HIDP_ERROR_CODES(0xC,1))
    //#define HIDP_STATUS_INVALID_REPORT_TYPE      (HIDP_ERROR_CODES(0xC,2))
    //#define HIDP_STATUS_INVALID_REPORT_LENGTH    (HIDP_ERROR_CODES(0xC,3))
    //#define HIDP_STATUS_USAGE_NOT_FOUND          (HIDP_ERROR_CODES(0xC,4))
    //#define HIDP_STATUS_VALUE_OUT_OF_RANGE       (HIDP_ERROR_CODES(0xC,5))
    //#define HIDP_STATUS_BAD_LOG_PHY_VALUES       (HIDP_ERROR_CODES(0xC,6))
    //#define HIDP_STATUS_BUFFER_TOO_SMALL         (HIDP_ERROR_CODES(0xC,7))
    //#define HIDP_STATUS_INTERNAL_ERROR           (HIDP_ERROR_CODES(0xC,8))
    //#define HIDP_STATUS_I8042_TRANS_UNKNOWN      (HIDP_ERROR_CODES(0xC,9))
    //#define HIDP_STATUS_INCOMPATIBLE_REPORT_ID   (HIDP_ERROR_CODES(0xC,0xA))
    //#define HIDP_STATUS_NOT_VALUE_ARRAY          (HIDP_ERROR_CODES(0xC,0xB))
    //#define HIDP_STATUS_IS_VALUE_ARRAY           (HIDP_ERROR_CODES(0xC,0xC))
    //#define HIDP_STATUS_DATA_INDEX_NOT_FOUND     (HIDP_ERROR_CODES(0xC,0xD))
    //#define HIDP_STATUS_DATA_INDEX_OUT_OF_RANGE  (HIDP_ERROR_CODES(0xC,0xE))
    //#define HIDP_STATUS_BUTTON_NOT_PRESSED       (HIDP_ERROR_CODES(0xC,0xF))
    //#define HIDP_STATUS_REPORT_DOES_NOT_EXIST    (HIDP_ERROR_CODES(0xC,0x10))
    //#define HIDP_STATUS_NOT_IMPLEMENTED          (HIDP_ERROR_CODES(0xC,0x20))

    /// <summary>
    /// Defines the return status for the <see cref="HidMethods"/>.
    /// </summary>
    public enum HidpStatus
    {
        HIDP_STATUS_SUCCESS                  = (0x0 << 28) | (0x11 << 16) | 0,
        HIDP_STATUS_NULL                     = (0x8 << 28) | (0x11 << 16) | 1,
        HIDP_STATUS_INVALID_PREPARSED_DATA   = (0xC << 28) | (0x11 << 16) | 1,
        HIDP_STATUS_INVALID_REPORT_TYPE      = (0xC << 28) | (0x11 << 16) | 2,
        HIDP_STATUS_INVALID_REPORT_LENGTH    = (0xC << 28) | (0x11 << 16) | 3,
        HIDP_STATUS_USAGE_NOT_FOUND          = (0xC << 28) | (0x11 << 16) | 4,
        HIDP_STATUS_VALUE_OUT_OF_RANGE       = (0xC << 28) | (0x11 << 16) | 5,
        HIDP_STATUS_BAD_LOG_PHY_VALUES       = (0xC << 28) | (0x11 << 16) | 6,
        HIDP_STATUS_BUFFER_TOO_SMALL         = (0xC << 28) | (0x11 << 16) | 7,
        HIDP_STATUS_INTERNAL_ERROR           = (0xC << 28) | (0x11 << 16) | 8,
        HIDP_STATUS_I8042_TRANS_UNKNOWN      = (0xC << 28) | (0x11 << 16) | 9,
        HIDP_STATUS_INCOMPATIBLE_REPORT_ID   = (0xC << 28) | (0x11 << 16) | 0xA,
        HIDP_STATUS_NOT_VALUE_ARRAY          = (0xC << 28) | (0x11 << 16) | 0xB,
        HIDP_STATUS_IS_VALUE_ARRAY           = (0xC << 28) | (0x11 << 16) | 0xC,
        HIDP_STATUS_DATA_INDEX_NOT_FOUND     = (0xC << 28) | (0x11 << 16) | 0xD,
        HIDP_STATUS_DATA_INDEX_OUT_OF_RANGE  = (0xC << 28) | (0x11 << 16) | 0xE,
        HIDP_STATUS_BUTTON_NOT_PRESSED       = (0xC << 28) | (0x11 << 16) | 0xF,
        HIDP_STATUS_REPORT_DOES_NOT_EXIST    = (0xC << 28) | (0x11 << 16) | 0x10,
        HIDP_STATUS_NOT_IMPLEMENTED          = (0xC << 28) | (0x11 << 16) | 0x20,
    }
}
