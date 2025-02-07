﻿using CalendarioFrontEnd.Auth;
using Microsoft.AspNetCore.Components.Authorization;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private static AuthenticationState NotAuthenticatedState = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal());

    private LoginUser _user;

    /// <summary>
    /// The display name of the user.
    /// </summary>
    public string DisplayName => this._user?.DisplayName;

    /// <summary>
    /// <see langword="true"/> if there is a user logged in, otherwise false.
    /// </summary>
    public bool IsLoggedIn => this._user != null;

    /// <summary>
    /// The current JWT token or <see langword="null"/> if there is no user authenticated.
    /// </summary>
    public string Token => this._user?.Jwt;

    /// <summary>
    /// Login the user with a given JWT token.
    /// </summary>
    /// <param name="jwt">The JWT token.</param>
    public void Login(string jwt)
    {
        var principal = JwtSerialize.Deserialize(jwt);
        this._user = new LoginUser(principal.Identity.Name, jwt, principal);
        this.NotifyAuthenticationStateChanged(Task.FromResult(GetState()));
    }

    /// <summary>
    /// Logout the current user.
    /// </summary>
    public void Logout()
    {
        this._user = null;
        this.NotifyAuthenticationStateChanged(Task.FromResult(GetState()));
    }

    /// <inheritdoc/>
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(GetState());
    }

    /// <summary>
    /// Constructs an authentication state.
    /// </summary>
    /// <returns>The created state.</returns>
    private AuthenticationState GetState()
    {
        if (this._user != null)
        {
            return new AuthenticationState(this._user.Claims);
        }
        else
        {
            return NotAuthenticatedState;
        }
    }
}