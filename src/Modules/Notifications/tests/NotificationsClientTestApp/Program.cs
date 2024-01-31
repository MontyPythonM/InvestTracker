﻿using InvestTracker.Shared.Abstractions.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

// arrange
var jwt = await GetJsonWebTokenAsync("http://localhost:5200/users-module/accounts/sign-in", 
    "investor@test.com", "test");

Console.WriteLine($"Response from API: {JsonConvert.SerializeObject(jwt)}");

// act
const string methodName = "Notify";
const string uri = "http://localhost:5200/notification-hub";

var connection = new HubConnectionBuilder()
    .WithUrl(uri, options =>
    {
        options.Headers.Add("Authorization", $"Bearer {jwt.AccessToken}");
    })
    .Build();

await connection.StartAsync();
Console.WriteLine($"Connection started with ID: {connection.ConnectionId}");

// assert
connection.On(methodName, (string message) =>
{
    Console.WriteLine($"Message: {message}");
});

while (true)
{
    await Task.Delay(1000);
}

async Task<JsonWebToken> GetJsonWebTokenAsync(string apiUri, string email, string password)
{
    using var httpClient = new HttpClient();
    var body = new { email, password };
    var stringContent = new StringContent(JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json");

    var response = await httpClient.PostAsync(apiUri, stringContent);

    var responseData = await response.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<JsonWebToken>(responseData) ?? throw new Exception("ERROR: Cannot deserialize JWT.");
}