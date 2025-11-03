import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrls: ['./register.css'],
})
export class Register {
  registerForm!: FormGroup;
  submitted = false;
  errorMessage = '';
  successMessage = '';

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {}

  ngOnInit() {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],

      phoneNo: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[0-9]{10}$/)   
        ]
      ],

      addressLine: ['', Validators.required],
      buildingName: ['', Validators.required],
      street: ['', Validators.required],

      postalCode: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[0-9]{6}$/)   
        ]
      ],

      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    });
  }

  get f() {
    return this.registerForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (this.registerForm.invalid) return;

    const data = this.registerForm.value;

    if (data.password !== data.confirmPassword) {
      this.errorMessage = 'Passwords do not match!';
      return;
    }

    const payload: any = {
      name: data.name,
      phoneno: data.phoneNo,
      addressLine: data.addressLine,
      buildingName: data.buildingName,
      street: data.street,
      postalCode: data.postalCode,
      password: data.password,
    };

    this.auth.postRegister(payload).subscribe({
      next: (res: any) => {
        this.successMessage = 'Registered Successfully!';
        this.router.navigate(['/login']);
      },
      error: (err: any) => {
        console.error('Register Error:', err);

        if (err.status === 409) {
          this.errorMessage = 'User already exists!';
        } else {
          this.errorMessage = 'Registration failed!';
        }
      }
    });
  }

  goToLogin() {
    this.router.navigate(['/login']);
  }
}
