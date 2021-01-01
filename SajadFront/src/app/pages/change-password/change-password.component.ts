import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { JWTTokenService } from 'src/app/services/jwttoken.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  public loginForm = new FormGroup({
    newPassword: new FormControl('', [Validators.required])
  });

  public get password(): AbstractControl { return this.loginForm.get('newPassword')  ?? new FormControl(); }
  public showLoading = false;
  public hidePassword = true;
  public userId: string = '';

  constructor(
    private service: AuthenticationService,
    private jwtTokenService: JWTTokenService,
    private snackBar: MatSnackBar,
    protected router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.userId = this.route.snapshot.params.userId;
  }

  public async login() {
    if (this.loginForm.valid) {

      const value = this.loginForm.value;
      value.userId = this.userId;
      try {
        this.showLoading = true;
        await this.service.changePassword(value);

        this.router.navigate(['Admin']);
      } finally {
        this.showLoading = false;
      }
    }
  }

}
