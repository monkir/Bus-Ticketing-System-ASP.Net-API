import { useEffect, useState } from "react"
import CustomerHeader from "../component/header"
import axios from "axios"
import Link from "next/link"
import CustomerFooter from "../component/footer"

const links = [
    { name: 'Open roles', href: '#' },
    { name: 'Internship program', href: '#' },
    { name: 'Our values', href: '#' },
    { name: 'Meet our leadership', href: '#' },
]
const stats = [
    { name: 'Offices worldwide', value: '12' },
    { name: 'Full-time colleagues', value: '300+' },
    { name: 'Hours per week', value: '40' },
    { name: 'Paid time off', value: 'Unlimited' },
]

export default function Example() {
    const [data, setData] = useState([])
    const [message, setMessage] = useState('This is message')

    async function fetchData(searchValue = "") {
        try {
            if (searchValue != "") {
                const response = await axios.get(
                    process.env.NEXT_PUBLIC_api_root + '/api/customer/ticket/search/' + searchValue,
                    {
                        headers: { 'Authorization': sessionStorage.getItem('token_string') }
                    }
                )
                setData(response.data)
            }
            else {
                const response = await axios.get(
                    process.env.NEXT_PUBLIC_api_root + '/api/customer/ticket/all',
                    {
                        headers: { 'Authorization': sessionStorage.getItem('token_string') }
                    }
                )
                setData(response.data)
            }

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
            <CustomerHeader title="Bus Ticketing System" pagename="Customer: My tickets" />
            <div className="overflow-x-auto px-10 min-h-[70vh]">
                <h1> {data.length == 0 ? "No data found" : ""} </h1>
                <table className="table table-zebra">
                    {/* head */}
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Ammount</th>
                            <th>Status</th>
                            <th>Trip_id</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map(item => (
                            <tr key={item.id}>
                                <th>{item.id}</th>
                                <td>{item.ammount}</td>
                                <td>{item.status}</td>
                                <td>{item.trip_id}</td>
                                <td><Link href={"/customer/ticket/details/" + item.id}>details</Link></td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <p className="text-2xl text-center">{message}</p>
            </div>
            <CustomerFooter/>
        </>

    )
}

