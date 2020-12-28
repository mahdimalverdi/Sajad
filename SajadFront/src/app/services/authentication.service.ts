import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Login } from '../models/login';
import { User } from '../models/user';
import { JWTTokenService } from './jwttoken.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private readonly client: HttpClient, private readonly jwtTokenService: JWTTokenService) { }

  public async login(model: Login): Promise<any> {
    var result = await this.client.post<any>('/api/Authentication/Login', model).toPromise();
    this.jwtTokenService.setToken(result.token);
  }

  public async register(model: Login): Promise<any> {
    await this.client.post<any>('/api/Authentication/Register', model).toPromise();
  }

  public async getUsers() {
    return await this.client.post<User[]>('/api/Authentication/GetUsers', null).toPromise();
  }
}
