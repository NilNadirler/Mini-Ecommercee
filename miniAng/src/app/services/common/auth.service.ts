import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private jwtHelper: JwtHelperService, private router: Router) {}

  idendtityCheck() {
    const token: string = localStorage.getItem('accessToken');

    // const decodeToken = this.jwtHelper.decodeToken(token);
    // const expirationDate: Data = this.jwtHelper.getTokenExpirationDate(token);
    let expired: boolean;

    try {
      expired = this.jwtHelper.isTokenExpired(token);
    } catch {
      expired = true;
    }

    _isAuthtenticated = token != null && !expired;
  }

  get isAuthtenticated(): boolean {
    return _isAuthtenticated;
  }
}

export let _isAuthtenticated: boolean;
