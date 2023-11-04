import { useEffect, useState } from "react"
import axios from "axios"
import Link from "next/link"
import BusProviderHeader from "../component/header"
import BusProviderFooter from "../component/footer"
import moment from "moment/moment"

export default function Example() {
    const [data, setData] = useState([])
    const [message, setMessage] = useState('This is message')
    const [account, setAccount] = useState()

    useEffect(()=>{
        if(data !== null){
            let sum = 0;
            data.map(x => {
                sum += x.amount;
            })
            setAccount(sum)
        }
    })

    async function fetchData(searchValue = "") {
        try {
            // if (searchValue != "") {
            //     const response = await axios.get(
            //         process.env.NEXT_PUBLIC_api_root + '/api/customer/ticket/search/' + searchValue,
            //         {
            //             headers: { 'Authorization': sessionStorage.getItem('token_string') }
            //         }
            //     )
            //     setData(response.data)
            // }
            // else {
                const response = await axios.get(
                    process.env.NEXT_PUBLIC_api_root + '/api/user/account/transaction',
                    {
                        headers: { 'Authorization': sessionStorage.getItem('token_string') }
                    }
                )
                setData(response.data)
            // }

        }
        catch (e) {
            try {
                console.log(e)
                setMessage(e.response.data.Message)
            }
            catch {
                console.log(e)
                setMessage("API is not connected")
            }
        }
    }

    useEffect(() => {
        fetchData();
    }, [])

    async function search(event) {
        // console.log(event?.target?.value)
        event.preventDefault()
        const searchValue = event?.target?.value
        fetchData(searchValue)
    }
    return (
        <>
            <BusProviderHeader title="Bus Ticketing System" pagename="Bus Provider: My Account" />
            <div className="overflow-x-auto px-10 min-h-[70vh]">
            <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
                <div className=" mx-auto space-x-2">
                    <span className=" text-xl text-blue-700">Account: {account}</span>
                    <Link className="btn btn-primary" href={'/busProvider/account/deposit'}>Depostit</Link>
                    <Link className="btn btn-secondary" href={'/busProvider/account/withdraw'}>Withdraw</Link>
                </div>
            </div>
                <h1> {data.length == 0 ? "No data found" : ""} </h1>
                <table className="table table-zebra">
                    {/* head */}
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Details</th>
                            <th>Ammount</th>
                            <th>Time</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map(item => (
                            <tr key={item.id}>
                                <th>{item.id}</th>
                                <td>{item.details}</td>
                                <td>{item.amount}</td>
                                <td>{item.time}</td>
                                <td>{moment(item.time, moment.HTML5_FMT.DATETIME_LOCAL_MS).format("DD/MM/YYYY hh:mm")}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <p className="text-2xl text-center">{message}</p>
            </div>
            <BusProviderFooter/>
        </>

    )
}

