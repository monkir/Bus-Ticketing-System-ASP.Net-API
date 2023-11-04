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
    const [id, setId] = useState();

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

    async function cancelData() {
        try {
          const response = await axios.post(
            process.env.NEXT_PUBLIC_api_root + '/api/customer/ticket/cancel/' + id, {},
            {
              headers: { 'Authorization': sessionStorage.getItem('token_string') }
            }
          )
          setMessage(response.data.message);
          fetchData();
        }
        catch (e) {
          try {
            console.log(e)
            setMessage(e.response.data.message)
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
                <p className="text-2xl text-center">{message}</p>
                <table className="table table-zebra">
                    {/* head */}
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Ammount</th>
                            <th>Trip_id</th>
                            <th>Action</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map(item => (
                            <tr key={item.id}>
                                <th>{item.id}</th>
                                <td>{item.ammount}</td>
                                <td>{item.trip_id}</td>
                                <td>{item.status}</td>
                                <td>
                                    <Link className=" btn btn-info mx-1"  href={"/customer/ticket/details/" + item.id}>details</Link>
                                    {
                                    item.status == 'booked'
                                    ? <span className=" btn btn-warning mx-1" onClick={() => { setId(item.id); document.getElementById('my_modal_1').showModal(); }} >
                                        Cancel
                                        </span>
                                    :item.status == 'cancalled'
                                    ? <span className=" btn btn-disabled mx-1" > Cancelled</span>
                                    :item.status == 'done'
                                    ? <span className=" btn btn-success mx-1" > Done</span>
                                    : ""


                                }
                                    

                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <CustomerFooter />
            {/* Delete Modal */}
            <dialog id="my_modal_1" className="modal">

                <div className="modal-box">
                    <h3 className="font-bold text-lg">Hello!</h3>
                    <p className="py-4">Sure to delete notice of id {id}</p>
                    <div className="modal-action">
                        <form method="dialog">
                            {/* if there is a button in form, it will close the modal */}
                            <button onClick={() => { setId(undefined); setMessage("Ticket is not cancelled") }} className="btn btn-info mx-1">No</button>
                            <button onClick={() => { cancelData() }} className="btn btn-warning mx-1">Sure</button>
                        </form>
                    </div>
                </div>
            </dialog>
        </>

    )
}

