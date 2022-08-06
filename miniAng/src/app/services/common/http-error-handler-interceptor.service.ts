import { ToastrService } from 'ngx-toastr';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpStatusCode,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpErrorHandlerInterceptorService implements HttpInterceptor {
  constructor(private toastrService: ToastrService) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        switch (error.status) {
          case HttpStatusCode.Unauthorized:
            this.toastrService.error('Yetkiniz yok');
            break;
          case HttpStatusCode.InternalServerError:
            this.toastrService.error('Sunucu Hastasi');
            break;
          case HttpStatusCode.BadRequest:
            this.toastrService.error('Gecersiz istek yapildi');
            break;
          case HttpStatusCode.NotFound:
            this.toastrService.error('Sayfa bulunamadi');
            break;
        }
        return of(error);
      })
    );
  }
}
