using System;
using Exemplo.Domain.Entities;

namespace Exemplo.Domain.Entities;

public class LogRequest : EntityBase
{
    #region "Constructors"
    public LogRequest(string rota, string controller, string httpVerb, string queryString, string requestBody, string responseBody, int usuarioId, string usuarioIp, int statusCode, long executionTime)
    {
        this.Ativar();
        this.Id = Id;
        Rota = rota;
        Controller = controller;
        HttpVerb = httpVerb;
        QueryString = queryString;
        RequestBody = requestBody;
        ResponseBody = responseBody;
        UsuarioId = usuarioId;
        UsuarioIP = usuarioIp;
        StatusCode = statusCode;
        ExecutionTime = executionTime;
    }
    #endregion

    #region "Properties"
    public string Rota { get; set; }
    public string Controller { get; set; }
    public string HttpVerb { get; set; }
    public string QueryString { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioIP { get; set; }
    public int StatusCode { get; set; }
    public string RequestBody { get; set; }
    public string ResponseBody { get; set; }
    public long ExecutionTime { get; set; }
    #endregion

    #region "References"
    #endregion

    #region "M?todos"
    #endregion
}









