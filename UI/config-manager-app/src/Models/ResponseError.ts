class ResponseError {
  constructor(
    public title: string,
    public status: number,
    public errors: unknown,
    public traceId: string,
    public type: string
  ) {
    this.title = title;
    this.status = status;
    this.errors = errors;
    this.traceId = traceId;
    this.type = type;
  }

  public static IsResponseError(obj: any): obj is ResponseError {
    return (
      obj &&
      typeof obj === "object" &&
      typeof obj.type === "string" &&
      typeof obj.title === "string" &&
      typeof obj.status === "number" &&
      typeof obj.traceId === "string"
    );
  }
}

export default ResponseError;
