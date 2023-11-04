import { useEffect, useState } from "react"
import axios from "axios"
import Link from "next/link"
import EmployeeHeader from "../../component/header"
import EmployeeFooter from "../../component/footer"

export default function Example() {
  const [data, setData] = useState([])
  const [message, setMessage] = useState('This is message')
  const [id, setId] = useState()

  async function fetchData(searchValue = "") {
    try {
      if (searchValue != "") {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/employee/trip/search/' + searchValue,
          {
            headers: { 'Authorization': sessionStorage.getItem('token_string') }
          }
        )
        setData(response.data)
      }
      else {
        const response = await axios.get(
          process.env.NEXT_PUBLIC_api_root + '/api/employee/trip/all/details',
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


  async function acceptAddingData() {
    try {
        console.log(sessionStorage.getItem('token_string'))
        const response = await axios.post(
          process.env.NEXT_PUBLIC_api_root + 'api/employee/trip/accept/add/'+id, {},
          {
            headers: { 'Authorization': sessionStorage.getItem('token_string') }
          }
        )
        setMessage(response.data.message)
        fetchData()

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

  async function acceptCancellingData() {
    try {
      const response = await axios.post(
        process.env.NEXT_PUBLIC_api_root + 'api/employee/trip/accept/cancel/' + id, {},
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
        setMessage(e.response.data.Message)
      }
      catch {
        console.log(e)
        setMessage("API is not connected")
      }
    }
  }

  async function acceptDoneData() {
    try {
      console.log(sessionStorage.getItem('token_string'))
      const response = await axios.post(
        process.env.NEXT_PUBLIC_api_root + 'api/employee/trip/done/' + id, {},
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
        setMessage(e.response.data.Message)
      }
      catch {
        console.log(e)
        setMessage("API is not connected")
      }
    }
  }

  async function search(event) {
    // console.log(event?.target?.value)
    event.preventDefault()
    const searchValue = event?.target?.value
    fetchData(searchValue)
  }
  return (
    <>
      <EmployeeHeader title="Bus Ticketing System" pagename="Employee: Manage trip"></EmployeeHeader>
      <div className="overflow-x-auto px-10 min-h-[70vh]">
        <div className="grid justify-items-stretch">
          <div className=" flex justify-self-center w-1/2">
            {/* <Link className="btn btn-active btn-outline w-1/4" href={'/busProvider/manage/trip/add'}>Add New Trip</Link> */}
            {/* Search Box */}
            <input
              type="text"
              name="search"
              className="block w-3/4 rounded-md border-0 py-1.5 pl-7 pr-20 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
              placeholder="search"
              onChange={search}
            />
          </div>
        </div>
        <h1 className="justify-center"> {data.length == 0 ? "No data found" : data.length + " data found "} </h1>
        <p className="text-2xl text-red-600 text-center">{message}</p>
        <h1> {data.length == 0 ? "No data found" : ""} </h1>
        <table className="table table-zebra">
          {/* head */}
          <thead>
            <tr>
              <th>ID</th>
              <th>Ticket Price</th>
              <th>Seat</th>
              <th>Status</th>
              <th>Start Time</th>
              <th>End Time</th>
              <th>Depot</th>
              <th>Destination</th>
              <th>Bus ID</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.id}>
                <th>{item.id}</th>
                <td>{item.ticketPrice} Tk</td>
                <td>{item.bookedSeat?.length}/40</td>
                <td>{item.status}</td>
                <td>{item.startTime}</td>
                <td>{item.endTime}</td>
                <td>{item.depot?.name}</td>
                <td>{item.destination?.name}</td>
                <td>{item.bus_id}</td>
                <td>
                  {
                    item.status == "adding-pending" ?
                      <button className=" btn btn-info mx-1" onClick={() => { setId(item.id); document.getElementById('acceptAdding_modal').showModal(); }} >
                        Accept Adding
                      </button>
                      : item.status == "cancelling-pending" ?
                        <button className=" btn btn-primary mx-1" onClick={() => { setId(item.id); document.getElementById('acceptCancelling_modal').showModal(); }} >
                          Accept Cancelling
                        </button>
                        : item.status == "added" ?
                          <button className=" btn btn-warning mx-1" onClick={() => { setId(item.id); document.getElementById('acceptDone_modal').showModal(); }} >
                            Apply Done
                          </button>
                          : "No Action"
                  }
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <p className="text-2xl text-center">{message}</p>
      </div>
      <EmployeeFooter />
      {/* acceptAdding Modal */}
      <dialog id="acceptAdding_modal" className="modal">
        <div className="modal-box">
          <h3 className="font-bold text-lg">Hello!</h3>
          <p className="py-4">Sure to add trip of id {id}</p>
          <div className="modal-action">
            <form method="dialog">
              {/* if there is a button in form, it will close the modal */}
              <button onClick={() => { setId(undefined); setMessage("Trip is not added") }} className="btn btn-info mx-1">Cancel</button>
              <button onClick={() => { acceptAddingData() }} className="btn btn-warning mx-1">Ok</button>
            </form>
          </div>
        </div>
      </dialog>
      {/* acceptCancelling Modal */}
      <dialog id="acceptCancelling_modal" className="modal">
        <div className="modal-box">
          <h3 className="font-bold text-lg">Hello!</h3>
          <p className="py-4">Sure to cancel trip of id {id}</p>
          <div className="modal-action">
            <form method="dialog">
              {/* if there is a button in form, it will close the modal */}
              <button onClick={() => { setId(undefined); setMessage("Trip is not cancelled") }} className="btn btn-info mx-1">Cancel</button>
              <button onClick={() => { acceptCancellingData() }} className="btn btn-warning mx-1">Ok</button>
            </form>
          </div>
        </div>
      </dialog>
      {/* acceptDone Modal */}
      <dialog id="acceptDone_modal" className="modal">
        <div className="modal-box">
          <h3 className="font-bold text-lg">Hello!</h3>
          <p className="py-4">Sure to done trip of id {id}</p>
          <div className="modal-action">
            <form method="dialog">
              {/* if there is a button in form, it will close the modal */}
              <button onClick={() => { setId(undefined); setMessage("Trip is not done") }} className="btn btn-info mx-1">Cancel</button>
              <button onClick={() => { acceptDoneData() }} className="btn btn-warning mx-1">Ok</button>
            </form>
          </div>
        </div>
      </dialog>
    </>
  )
}

