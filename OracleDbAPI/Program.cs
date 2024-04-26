using Oracle.ManagedDataAccess.Client;
using OracleDbAPI;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
//CREATE TABLE "ADMIN"."USERS" ("NAME" VARCHAR2(100),"AGE" NUMBER(3,0))
//required for mTLS port is 1522, for TLS port is 1521
//OracleConfiguration.TnsAdmin = "C:\\Users\\Administrator\\Oracle\\network\\admin\\<DBNAME>";
//OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;
//(description= (retry_count=20)(retry_delay=3)(address=(protocol=tcps)(port=1521)(host=adb.ap-hyderabad-1.oraclecloud.com))(connect_data=(service_name=bn5fk4w1csnyf6r_j9sw7dr3incogrbl_high.adb.oraclecloud.com))(security=(ssl_server_dn_match=yes)))
string dbconn = "Data Source=(description= (retry_count=20)(retry_delay=3)(address=(protocol=tcps)(port=1521)(host=adb.ap-hyderabad-1.oraclecloud.com))(connect_data=(service_name=bn5fk4w1csnyf6r_j9sw7dr3incogrbl_high.adb.oraclecloud.com))(security=(ssl_server_dn_match=yes)));User Id=<USERNAME>;Password=<PASSWORD>;";
builder.Services.AddSingleton(new DatabaseService(dbconn));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Oracle API", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oracle API");
});

app.MapControllers();

app.Run();
