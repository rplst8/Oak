﻿module uimethods

open setupmethods

open runner

open canopy

let baseUrl = "http://localhost:3000";

let url = fun address -> canopy.url (baseUrl + address)

let on = fun address -> canopy.on (baseUrl + address)

let logOff = fun _ ->
    url "/account/logoff"
    ()

let goToSignIn = fun _ ->
    url "/"
    ()

let registerUser = fun email ->
    goToSignIn()
    click "a[href='/Account/Register']"
    on "/Account/Register"
    write "#Email" email
    write "#Password" "Password"
    write "#PasswordConfirmation" "Password"
    click "input[value='register']"
    on "/"
    logOff()

let loginAs = fun email ->
    goToSignIn()
    write "#Email" email
    write "#Password" "Password"
    click "input[value='login']"
    on "/"

let addGame = fun name ->
    click "#showLibrary"
    write "#gameToAdd" name
    System.Threading.Thread.Sleep(100)
    click "table tbody tr td"
    click "#closeLibraryTop"