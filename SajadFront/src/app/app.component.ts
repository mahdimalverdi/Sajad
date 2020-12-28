import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JWTTokenService } from './services/jwttoken.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'SajadFront';

  constructor(
    private readonly jwtTokenService: JWTTokenService,
    private readonly router: Router) {
  }

  public get token(){
    return this.jwtTokenService.getToken();
  }

  public logout(){
    this.jwtTokenService.removeToken();
    this.router.navigate(['/']);
  }
}
