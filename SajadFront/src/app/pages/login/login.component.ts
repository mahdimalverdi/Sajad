import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { JWTTokenService } from 'src/app/services/jwttoken.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });

  public get userName(): AbstractControl { return this.loginForm.get('userName') ?? new FormControl(); }
  public get password(): AbstractControl { return this.loginForm.get('password')  ?? new FormControl(); }
  public showLoading = false;
  public hidePassword = true;

  constructor(
    private service: AuthenticationService,
    private jwtTokenService: JWTTokenService,
    private snackBar: MatSnackBar,
    protected router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

  public async login() {
    if (this.loginForm.valid) {

      const value = this.loginForm.value;

      try {
        this.showLoading = true;
        await this.service.login(value);
        const name = this.jwtTokenService.getUser();

        this.snackBar.open(`${name} خوش آمدید!`, 'بستن', {
          duration: 5000,
        });

        const role = this.jwtTokenService.getRole();

        if (role === 'Admin') {
          this.router.navigate(['Admin']);
        } else {
          this.router.navigate(['AddQuestion']);
        }
      } finally {
        this.showLoading = false;
      }
    }
  }

}
