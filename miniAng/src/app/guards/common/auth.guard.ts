import {
  AuthService,
  _isAuthtenticated,
} from './../../services/common/auth.service';
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
    private spinner: NgxSpinnerService,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    this.spinner.show(SpinnerType.BallAtom);

    if (!_isAuthtenticated) {
      this.router.navigate(['login'], {
        queryParams: { returnUrl: state.url },
      });

      this.toastrService.warning('Oturumunuz acmaniz gerekiyor!');
    }

    this.spinner.hide(SpinnerType.BallAtom);

    return true;
  }
}
