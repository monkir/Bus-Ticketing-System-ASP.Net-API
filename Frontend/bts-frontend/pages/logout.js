import axios from "axios"
import { useRouter } from "next/router"
import { useEffect, useState } from "react"
import MyFooter from "./component/footer";
import MyHeader from "./component/header";

export default function logout() {
    const [message, setMessage] = useState('This is message')
    const router = useRouter();
    async function userLogout () {
        try {
            const response = await axios.post(process.env.NEXT_PUBLIC_api_root + '/api/user/logout', {},
                {
                    headers: { 'Authorization': sessionStorage.getItem('token_string') }
                })
            setMessage(response.data.Message)
            sessionStorage.clear();
            setTimeout(() => {
                router.push('/')
            }, 2000);
        } catch (e) {
            try {
                setMessage(e.response.data.message)
            } catch {
                setMessage("Unkown error occured")
                console.log(e)
            }
        }
    }
    useEffect(() => { userLogout() }, [])
    return (
        <>
            {/* <MyHeader /> */}
            <div className=" w-full h-[70vh]">
                <div className=" mx-auto my-auto">{message}</div>
            </div>
            {/* <MyFooter /> */}
        </>
    )
}