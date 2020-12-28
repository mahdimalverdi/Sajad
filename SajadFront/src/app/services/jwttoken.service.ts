import { Injectable } from '@angular/core';
import * as jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class JWTTokenService {
  private jwtToken: string;
  private decodedToken: { [key: string]: string } = {};

  private readonly jwtTokenKey = 'JWTToken';

  constructor() {
    this.jwtToken = this.getToken();
  }

  public removeToken() {
    localStorage[this.jwtTokenKey] = undefined;

    this.jwtToken = '';
    this.decodedToken = {};
  }

  public setToken(token: string) {
    if (token) {
      this.jwtToken = token;
      localStorage[this.jwtTokenKey] = token;
    }
  }

  public getToken() {
    return localStorage[this.jwtTokenKey];
  }

  public decodeToken() {
    if (this.jwtToken) {
      this.decodedToken = jwt_decode.default(this.jwtToken);
    }
  }

  public getDecodeToken() {
    return jwt_decode.default(this.jwtToken);
  }

  public getUser() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.firstName ?? this.decodedToken.unique_name : null;
  }

  public getRole() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.role : null;
  }

  public getEmailId() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.email : null;
  }

  public getExpiryTime(): number {
    this.decodeToken();

    const expiryTime = this.decodedToken ? this.decodedToken.exp : 1000;
    const num = +expiryTime;

    return num;
  }

  public isTokenExpired(): boolean {
    const expiryTime: number = this.getExpiryTime();
    if (expiryTime) {
      return ((1000 * expiryTime) - (new Date()).getTime()) < 5000;
    } else {
      return false;
    }
  }
}
