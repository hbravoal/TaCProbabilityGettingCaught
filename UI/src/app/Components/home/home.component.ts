import { FormControl, FormGroup, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/Services/api.service';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public progress: number;
  public message: string;
  formTest: FormGroup;
blob: any;
loading: boolean;
  constructor(private apiService: ApiService) { 
    
    this.formTest = new FormGroup({
      'Identification' : new FormControl('', [Validators.required]),
      'fileData': new FormControl(null, [Validators.required])
    });
    
  }

  ngOnInit() {
  }

  onFileSelect(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.formTest.get('fileData').setValue(file);
    }
  }

  onSubmit() {
    
    this.loading = true;
    const formData = new FormData();
    formData.append('file', this.formTest.get('fileData').value);    
    formData.append('Identification', this.formTest.get('Identification').value);    
  
  
    this.apiService.UploadFile(formData).subscribe((data:any) => {
      //save it on the client machine.
     
  if(data.isSucess){
    console.log(environment.baseUrl+  data.result); 
  }else{
    console.log('Hubo un error');
  }
      
      
      this.loading= false;
    })
  }

}

