import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { JWTTokenService } from '../services/jwttoken.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeGuard {

  constructor(
    protected readonly router: Router,
    protected readonly jwtService: JWTTokenService,
    protected readonly activatedRoute: ActivatedRoute) {
  }

  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
    if (this.jwtService.getUser()) {
      if (this.jwtService.isTokenExpired()) {
        this.jwtService.removeToken();
        this.navigateToLogin(state.url);
      } else {
        return true;
      }
    } else {
      this.navigateToLogin(state.url);
    }
    return false;
  }

  private navigateToLogin(currentUrl: string) {
    this.router.navigate([`/Login`], { queryParams: { returnUrl: currentUrl } });
  }
}
