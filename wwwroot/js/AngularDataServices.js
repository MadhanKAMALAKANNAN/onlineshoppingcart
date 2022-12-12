////(function () {
////    'use strict';

////    angular
////        .module('app')
////        .controller('RolesController', RolesController);

////    RolesController.$inject = ['$location'];

////    function RolesController($location) {
////        /* jshint validthis:true */
////        var vm = this;
////        vm.title = 'RolesController';

////        activate();

////        function activate() { }
////    }
////})();
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Roles } from '../model/Roles.model';
 

@Injectable({
    providedIn: 'root'
})
/*export class AngularDataServices {*/
export class AngularDataServices{
    readonly url = 'http://localhost:5001/Admin/';
    listRoles: Roles[];

    constructor(private http: HttpClient) { }

   GetRoles() {
       this.http.get(this.url + 'GetRoles').toPromise().then(result => this.listRoles = result as Roles[])
    }   

}

export class InitDataClass implements OnInit {
    constructor(private service: AngularDataServices) {
    }
    ngOnInit() {
        this.service.GetRoles();
    }