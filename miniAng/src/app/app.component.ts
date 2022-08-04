import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from './services/common/auth.service';
import { Component } from '@angular/core';

declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(
    public authService: AuthService,
    private toastrService: ToastrService,
    private router: Router
  ) {
    authService.idendtityCheck();
  }

  signOut() {
    localStorage.removeItem('accessToken');
    this.authService.idendtityCheck();
    this.toastrService.warning('Oturum kapatilmistir');
    this.router.navigate(['']);
  }
}
