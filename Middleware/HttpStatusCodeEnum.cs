using Microsoft.AspNetCore.Http.HttpResults;

namespace infertility_system.Middleware
{
    public enum HttpStatusCodeEnum
    {
        Ok = 200, //thành công
        BadRequest = 400, //yêu cầu k hợp lệ
        Unauthorized = 401, //chưa xác thực quyền truy cập
        Forbidden = 403, //Bị cấm truy cập dù đã xác thực
        NotFound = 404, // không tìm thấy
        InternalServerError = 500 //lỗi máy chủ
    }
}
