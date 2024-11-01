// export default function test(){
//     return <>
//     {/* xs */}
//     <input type="text" placeholder="Type here" className="input input-bordered input-xs w-full max-w-xs" /><br/>
//           {/* sm */}
//           <input type="text" placeholder="Type here" className="input input-bordered input-sm w-full max-w-xs" /><br/>
//           {/* md */}
//           <input type="text" placeholder="Type here" className="input input-bordered input-md w-full max-w-xs" /><br/>
//           {/* lg */}
//           <input type="text" placeholder="Type here" className="input input-bordered input-lg w-full max-w-xs" /><br/>
//     </>
// }
export default function test() {
    return (
        <>
            <div className="toast toast-top toast-start">
                <div className="alert alert-info">
                    <span>New mail arrived.</span>
                </div>
                <div className="alert alert-success">
                    <span>Message sent successfully.</span>
                </div>
            </div>
            This is a message
        </>
    )
}