namespace CDOProspectClient.Contracts;

public record BaseResponse<T>(
    bool IsSuccess,
    List<string> Errors,
    T Data
);