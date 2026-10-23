import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet, 
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  //attributes
  endpoint: string = 'https://localhost:7157/jobs';
  MessageOperationPerformed: string = 'Operation carried out successfully';
  jobs: any[] = [];
  applicants: any[] = []; 
  
  constructor(
   
    private httpClient: HttpClient
  ) {}


  form = new FormGroup({
    //campo 'Title'
    title : new FormControl('', [
      Validators.required, Validators.minLength(8), Validators.maxLength(150)
    ]),
    //campo 'ContactEmail'
    contactEmail : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'ContactPhone'
    contactPhone : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'Description'
    description : new FormControl('', [
      Validators.required, Validators.minLength(8), Validators.maxLength(255)
    ]),
    //campo 'Country'
    country : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'City'
    city : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'Manager'
    manager : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'RequiredSkills'
    requiredSkills : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'JobType'
    jobType : new FormControl('', [
       Validators.maxLength(255)
    ]),
    //campo 'JobExperience'
    jobExperience : new FormControl('', [
      Validators.maxLength(255)
    ]),
      //campo 'SalaryFrom'
      salaryFrom : new FormControl('', [
        Validators.maxLength(255)
      ]),
    //campo 'SalaryTo'
    salaryTo : new FormControl('', [
      Validators.maxLength(255)
    ])
});


  formEdicao = new FormGroup({
    id : new FormControl(''),
    //campo 'Title'
    title : new FormControl('', [
      Validators.required, Validators.minLength(8), Validators.maxLength(150)
    ]),
    //campo 'ContactEmail'
    contactEmail : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'ContactPhone'
    contactPhone : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'Description'
    description : new FormControl('', [
      Validators.required, Validators.minLength(8), Validators.maxLength(255)
    ]),
    //campo 'Country'
    country : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'City'
    city : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'Manager'
    manager : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'RequiredSkills'
    requiredSkills : new FormControl('', [
      Validators.maxLength(255)
    ]),
    //campo 'JobType'
    jobType : new FormControl('', [
       Validators.maxLength(255)
    ]),
    //campo 'JobExperience'
    jobExperience : new FormControl('', [
      Validators.maxLength(255)
    ]),
      //campo 'SalaryFrom'
      salaryFrom : new FormControl('', [
        Validators.maxLength(255)
      ]),
    //campo 'SalaryTo'
    salaryTo : new FormControl('', [
      Validators.maxLength(255)
    ])
  });

   //function to display validation messages
   //for each of the form fields
  get f() {
    return this.form.controls;
  }

   //function to display validation messages
   //for each of the form fields
  get fEdicao() {
    return this.formEdicao.controls;
  }
  
   //method executed whenever the component
   //is opened (initialized)
  ngOnInit(): void {

  
    this.httpClient.get(this.endpoint)
      .subscribe({ 
        next: (data) => { 
        
          this.jobs = data as any[];
        },
        error: (e) => { 
          console.log(e.error);
        }
      })
  }


  onSubmit() : void {
    

    this.httpClient.post(this.endpoint, this.form.value)
      .subscribe({ 
        next: (data: any) => { 
        
          alert(this.MessageOperationPerformed);          
          this.form.reset();
          this.ngOnInit();
        },
        error: (e) => { 
          console.log(e);
     
        }
      })
  }


  onDelete(id: string) : void {

 
    if(confirm('Do you really want to delete the contact?')) {

   
      this.httpClient.delete(this.endpoint + "/" + id)
        .subscribe({
          next: (data: any) => {
            alert(this.MessageOperationPerformed); 
            this.ngOnInit(); 
          },
          error: (e) => {
            console.log(e.error);
          }
        });
    }
  }


  onEdit(id: string) : void {


    this.httpClient.get(this.endpoint + "/" + id)
      .subscribe({
        next: (data: any) => {
         
          this.formEdicao.patchValue(data);
        },
        error: (e) => {
          console.log(e.error);
        }
      });
  }
  onViewApplicants(id: string) : void {


    this.httpClient.get(this.endpoint + "/" + id)
      .subscribe({
        next: (data: any) => {      
          this.applicants = data.applicants as any[];
        },
        error: (e) => {
          console.log(e.error);
        }
      });
  }


  onEditSubmit(): void {
    this.httpClient.put(this.endpoint, this.formEdicao.value)
      .subscribe({
        next: (data: any) => {
          alert(this.MessageOperationPerformed); 
          this.ngOnInit(); 
        },
        error: (e) => {
          console.log(e.error);
        }
      });
  }

}
