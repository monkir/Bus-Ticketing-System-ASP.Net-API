import { useEffect, useState } from "react"
import CustomerHeader from "../component/header"
import axios from "axios"
import Link from "next/link"

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
                    process.env.NEXT_PUBLIC_api_root + '/api/customer/trip/search/' + searchValue,
                    {
                        headers: { 'Authorization': sessionStorage.getItem('token_string') }
                    }
                )
                setData(response.data)
            }
            else {
                const response = await axios.get(
                    process.env.NEXT_PUBLIC_api_root + '/api/customer/trip/my',
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
            <CustomerHeader title="Bus Ticketing System" pagename="Customer: My trips" />
            <div className="overflow-x-auto px-10 min-h-[70vh]">
                {/* Search Box */}
                <div className="grid justify-items-stretch">
                    <div className=" flex justify-self-center w-1/4">
                        <Link className="btn btn-active btn-outline w-full" href={'/customer/trip'}>Find Trips</Link>
                        {/* <input
                            type="text"
                            name="search"
                            className="block w-3/4 rounded-md border-0 py-1.5 pl-7 pr-20 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                            placeholder="search"
                            onChange={search}
                        /> */}
                    </div>
                </div>
                <h1> {data.length == 0 ? "No data found" : ""} </h1>
                <table className="table table-zebra">
                    {/* head */}
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>From</th>
                            <th>To</th>
                            <th>Start at</th>
                            <th>End at</th>
                            <th>Price</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map(item => (
                            <tr key={item.id}>
                                <th>{item.id}</th>
                                <td>{item.depot.name}</td>
                                <td>{item.destination.name}</td>
                                <td>{item.startTime}</td>
                                <td>{item.endTime}</td>
                                <td>{item.ticketPrice}</td>
                                <td><Link href={"/customer/trip/book/" + item.id}>Book</Link></td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <p className="text-2xl text-center">{message}</p>
            </div>
        </>

    )
}

