import { SpinnerType } from './../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

import {
  ActivatedRouteSnapshot,
  CanActivate,
  Data,
  Router,
  RouterStateSnapshot,
} from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router,
    private toastrService: ToastrService,
    private spinner: NgxSpinnerService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.spinner.show(SpinnerType.BallAtom);

    const token: string = localStorage.getItem('accessToken');

    // const decodeToken = this.jwtHelper.decodeToken(token);
    // const expirationDate: Data = this.jwtHelper.getTokenExpirationDate(token);
    let expired: boolean;

    try {
      expired = this.jwtHelper.isTokenExpired(token);
    } catch {
      expired = true;
    }

    if (!token || expired) {
      this.router.navigate(['login'], {
        queryParams: {
          returnUrl: state.url,
        },
      });

      this.toastrService.error('Oturumunuz acmaniz gerekiyor! Yetkisiz erisim');
    }

    this.spinner.hide(SpinnerType.BallAtom);
    return null;
  }
}
