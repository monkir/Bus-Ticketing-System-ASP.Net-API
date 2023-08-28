import { redirect } from "next/dist/server/api-utils";
import MyFooter from "./component/footer"
import MyHeader from "./component/header"
import axios from "axios"
import { useState } from "react";
import { useRouter } from "next/router";
import { headers } from "@/next.config";
  
  export default function Example() {
    const [info, setInfo] = useState('');
    const router = useRouter()
    async function getMyInfo(event) {
      try{
        const response = await axios.get(
            'https://localhost:44304/api/user/profile',
            {
                headers: {'Authorization': sessionStorage.getItem('token_string')}
            }
        )
        console.log(response.data)
      }
      catch(e){
          console.log(e)
          setInfo(e.response.data.Message)
      }
      
    }
    return (
        <>
        <MyHeader title="Login - Bus Ticketing System"/>
        <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
          <div className="sm:mx-auto sm:w-full sm:max-w-sm">
            <img
              className="mx-auto h-10 w-auto"
              src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
              alt="Your Company"
            />
            <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
              Sign in to your account
            </h2>
            <button className="btn items-center" onClick={getMyInfo}>Get My Information</button>
            <p className="mt-10 text-center text-sm text-gray-500">
              {info}
            </p>
          </div>

          <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
          </div>
        </div>
        <MyFooter/>
        </>
    )
  }
  
  