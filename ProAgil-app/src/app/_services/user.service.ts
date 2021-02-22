import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/User';



@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseURL = 'http://localhost:5000/api/user/';

constructor(private http: HttpClient) { }











}
