import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { JWTTokenService } from '../services/jwttoken.service';

@Injectable()
export class UrlInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (!request.url.toLowerCase().startsWith('http')) {
      request = request.clone({
        url: environment.baseUrl + request.url
      });
    }

    return next.handle(request);
  }
}
