import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';
import { Customer } from '../models/customer.model';
import { AsyncPipe, NgIf } from '@angular/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule, AsyncPipe, ReactiveFormsModule, NgIf],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

  deleteSucceedMSG = false;
  ADDSucceedMSG = false;

  http = inject(HttpClient);
  customers$ = this.getCustomers();
  customerForm = new FormGroup ({
    name: new FormControl<string>("", Validators.required),
    email: new FormControl<string>("", [Validators.required, Validators.email]),
    phone: new FormControl<string>("", Validators.required),
    address: new FormControl<string>("", Validators.required)
  })

  // delete a Customer
  deleteCustomer(id: string) {    
    this.http.delete(`https://localhost:7101/api/Customers/${id}`).subscribe({
      next: (value) => {
        console.log("delete: "+ value);
        this.update();
        this.customerForm.reset();
        this.deleteSucceedMSG = true;
        this.closeDeleteMSG();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  editCustomer(customer: Customer) {
    // yet to implement
    console.log(`Edit customer: ${customer.id}`);
  }

  // add a New Customer
  onSubmit() {
    const addNewCustomer =  {
      name: this.customerForm.value.name,
      email: this.customerForm.value.email,
      address: this.customerForm.value.address,
      phone: this.customerForm.value.phone
    }
    this.http.post("https://localhost:7101/api/Customers", addNewCustomer)
    .subscribe({
      next: (value) => {
        console.log(value);
        this.update();
        this.ADDSucceedMSG = true;
        this.closeAddMSG();
        this.customerForm.reset();
      },
      error: (err) => {
        console.log(err);
      }}      
    )
  }

  // load Customers list
  private getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>("https://localhost:7101/api/Customers");
  }





    update() {
    this.customers$ = this.getCustomers();
  }

  closeDeleteMSG(now:boolean = false) {
    if (now) {
      this.deleteSucceedMSG = false;
      return;
    }
    setTimeout(() => {
      this.deleteSucceedMSG = false;
    }, 3000);
  }
  closeAddMSG(now:boolean = false) {
    if (now) {
      this.ADDSucceedMSG = false;
      return;
    }
    setTimeout(() => {
      this.ADDSucceedMSG = false;
    }, 3000);
  }
}
